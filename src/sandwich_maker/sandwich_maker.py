"""
Code for sandwich maker
"""

from __future__ import print_function

# Import libraries necessary to connect to dynamoDB
import boto3
from botocore.exceptions import ClientError
from boto3 import dynamodb
from boto3.session import Session

# Import other libraries
import json
import decimal
import time
import serial
import sys
import datetime
import re

# Constants for table
STATUS_PENDING = 'pending'
STATUS_WAITING = 'waiting'
STATUS_DELIVERING = 'delivering'

# Column names for table
COL_CUSTOMERID = 'CustomerId'
COL_INGREDIENTS = 'Ingredients'
COL_TIMESTAMP = 'Timestamp'
COL_LOCATION = 'Location'
COL_STATUS = 'OrderStatus'

# DynamoDB Access information
# Read key information from config file
with open('config.json') as json_data_file:
    config_data = json.load(json_data_file)
AWS_KEY = config_data['AWS_KEY']
AWS_PASS = config_data['AWS_PASS']
DYNAMODB_REGION = config_data['DYNAMODB_REGION']
DYNAMODB_ENDPOINT_URL = 'https://dynamodb.aws-east-1.amazonaws.com'
dynamodb_session = Session(aws_access_key_id=AWS_KEY,
              aws_secret_access_key=AWS_PASS,
              region_name=DYNAMODB_REGION)
dynamodb = dynamodb_session.resource('dynamodb')

# Helper class to convert a DynamoDB item to JSON.
class DecimalEncoder(json.JSONEncoder):
    def default(self, o):
        if isinstance(o, decimal.Decimal):
            if o % 1 > 0:
                return float(o)
            else:
                return int(o)
        return super(DecimalEncoder, self).default(o)

# Retrieve next order from table
def table_get_all_orders():
    table = dynamodb.Table('Orders')

    response = table.scan()
    print("Scan succeeded:")
    #print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Insert order into table
def table_insert_order(customerid, timestamp, ingredients, location):
    table = dynamodb.Table('Orders')

    response = table.put_item(
        Item={
            COL_CUSTOMERID: customerid,
            COL_TIMESTAMP: timestamp,
            COL_INGREDIENTS: ingredients,
            COL_LOCATION: location,
            COL_STATUS: STATUS_PENDING
        }
    )
    print("PutItem succeeded:")
    print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Retrieve orders from table using customerid
def table_get_order(customerid):
    table = dynamodb.Table('Orders')

    response = table.get_item(
        Key={
            COL_CUSTOMERID: customerid
        }
    )
    print("GetItem succeeded:")
    print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Retrieve orders from table using customerid
def table_delete_order(customerid):
    table = dynamodb.Table('Orders')

    response = table.delete_item(
        Key={
            COL_CUSTOMERID: customerid
        }
    )
    print("DeleteItem succeeded:")
    # print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Update order in table
def update_order(order):
    table = dynamodb.Table('Orders')
    response = table.update_item(
        Key = {
            COL_CUSTOMERID: order[COL_CUSTOMERID]
        },
        UpdateExpression = ('SET ' + COL_STATUS + ' = :val'),
        ExpressionAttributeValues={
            ':val': order[COL_STATUS]
        }
    )

    print("UpdateItem succeeded:")
    # print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response


# Check if any orders are delivering or waiting
def is_delivering_order():
    json_response = table_get_all_orders()
    num_orders = json_response['Count'];
    if (num_orders != 0):
        for order in json_response["Items"]:
            if order[COL_STATUS] == STATUS_DELIVERING or order[COL_STATUS] == STATUS_WAITING:
                return True
    # Return false if no orders are delivering
    return False

# Find next order to deliver based on time and pending status
def find_next_order(orders):
    index_earliest = 0
    time_earliest = datetime.datetime(2100,1,1)
    for idx, order in enumerate(orders):
        time_order = datetime.datetime.strptime(order[COL_TIMESTAMP], "%Y-%m-%dT%H:%M:%SZ")
        if (time_order < time_earliest and order[COL_STATUS] == 'pending'):
            index_earliest = idx
            time_earliest = time_order
    print ("Next order index: " + str(index_earliest))
    return orders[index_earliest]

# Helper function to parse list of ingredients and conver to code for dispenser
ingredients_valid = ['lettuce', 'tomatoes', 'cheese', 'onions']
ingredient_value = {
    'lettuce': 1,
    'tomatoes': 2,
    'cheese': 4,
    'onions': 8
}
negative_token = ['no', 'without']
or_operator = 'or'

# Parse order string
def parse_ingredients(order_string):

    # Split into words
    token_list = order_string.split()

    # List of ingredients obtained from order
    ingredients_list = []

    # Flag to skip next ingredient
    skip_next = False

    # Flag to record if previous ingredient was skipped
    skip_prev = False

    # Iterate through each word
    for token in token_list:

        # If token is a valid ingredient name
        if (token in ingredients_valid):
            # Keep track of previous skip value
            skip_prev = skip_next

            # If skip next flag is set, do not add to list of ingredients
            if (skip_next):
                skip_next = False
            # Otherwise, add to ingredients list
            else:
                ingredients_list.append(token)

        # If token is a negative word, skip next ingredient
        if (token in negative_token):
            # Skip next ingredient
            skip_next = True

        # If token is an 'or', apply same skip flag as previous ingredient
        if (token == or_operator):
            # Skip next ingredient
            skip_next = skip_prev

    # Calculate ingredient code to be sent to microcontroller
    code = 0
    for ingredient in ingredients_list:
        code += ingredient_value[ingredient]

    return code

# Dispatch order by sending commands to microcontroller
def dispatch_order(order):
    print ("Sandwich with " + order[COL_INGREDIENTS] + " delivering to " + order[COL_LOCATION])
    ingredient_code = parse_ingredients(order[COL_INGREDIENTS])
    print ("Running command \"/m_sand/run " + str(ingredient_code) + "\"")
    ser.write("/m_sand/run " + str(ingredient_code))
    response = ser.readline()
    print (response) # Wait until mbed resopnds that sandwich is made
    while ("Made sandwich" not in response):
        response += ser.readline()
        time.sleep(1)
        print (response)
    print ("Done making sandwich")

# Send next order by dispatching order to microcontroller and updating status
def send_next_order():
    json_response = table_get_all_orders()
    num_orders = json_response['Count'];
    if (num_orders != 0):
        print ("Found next order ...")
        next_order = find_next_order(json_response["Items"])

        # Dispatch order to dispenser
        dispatch_order(next_order)

        # Update status of order to delivering
        next_order[COL_STATUS] = STATUS_WAITING
        update_order(next_order)

        # Return true if order updated successfully
        return True
    else: # Else, return false
        return False

# Attempt to open serial connection
ser = serial.Serial()
ser.baudrate = 9600
ser.port = 'COM4'
ser.open()
if(not ser.is_open):
    print("Error opening COM port")
    sys.exit(1)

# Main loop that checks for orders and executes them
try:
    while True:
    # Check if delivering any orders
    # If not, send a new order
        if (not is_delivering_order()):
            send_next_order()
            print ("Order sent ...")
        else:
            print ("Delivering now ...")
        time.sleep(10) # sleep for 10 seconds (not overload database server)

# Keyboard interrupt (Ctrl-C) pressed, exit program
except KeyboardInterrupt:
    ser.close()
    print ("Exited Program\n")
    sys.exit(0)
