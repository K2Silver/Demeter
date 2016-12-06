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
import os

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
with open(os.path.dirname(os.path.realpath(__file__)) + '\config.json') as json_data_file:
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
    #print("Scan succeeded:")
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
    #print("PutItem succeeded:")
    #print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Retrieve orders from table using customerid
def table_get_order(customerid):
    table = dynamodb.Table('Orders')

    response = table.get_item(
        Key={
            COL_CUSTOMERID: customerid
        }
    )
    #print("GetItem succeeded:")
    #print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Retrieve orders from table using customerid
def table_delete_order(customerid):
    table = dynamodb.Table('Orders')

    response = table.delete_item(
        Key={
            COL_CUSTOMERID: customerid
        }
    )
    # print("DeleteItem succeeded:")
    # print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Update order in table
def table_update_order(order):
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

    # print("UpdateItem succeeded:")
    # print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# Check if any orders are waiting to be delivered
def get_next_order_to_deliver():
    json_response = table_get_all_orders()
    num_orders = json_response['Count'];
    if (num_orders != 0):
        for order in json_response["Items"]:
            if order[COL_STATUS] == STATUS_WAITING:
                return order
    # Return false if no orders are waiting to be delivered
    return None

# Check if any orders are delivering
def get_order_delivering():
    json_response = table_get_all_orders()
    num_orders = json_response['Count'];
    if (num_orders != 0):
        for order in json_response["Items"]:
            if order[COL_STATUS] == STATUS_DELIVERING:
                return order
    # Return None if no orders are delivering
    return None

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

# Execute command

# Delete item in table with delivering status
if (sys.argv[1] == "DeleteDelivered"):
    order_to_delete = get_order_delivering();
    if (order_to_delete != None):
        table_delete_order(order_to_delete[COL_CUSTOMERID]);
        print ("Deleted")
        exit(0)
    else:
        print ("Not deleted")
        exit(1)

# Check for next order that is waiting to be delivered, and set to delivering status
elif (sys.argv[1] == "CheckNextOrder"):
    order_to_deliver = get_next_order_to_deliver();
    if (order_to_deliver != None):
        order_to_deliver[COL_STATUS] = STATUS_DELIVERING
        table_update_order(order_to_deliver)
        print (order_to_deliver[COL_LOCATION])
        exit(0)
    print ("NoOrdersReady")
    exit(0)
