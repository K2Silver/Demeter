"""
Order maker skill
"""

from __future__ import print_function

# Import libraries necessary to connect to dynamoDB
import boto3
from botocore.exceptions import ClientError
import json
import decimal
import time
from boto3 import dynamodb
from boto3.session import Session

# Constants for table
STATUS_PENDING = 'pending'
STATUS_DELIVERING = 'delivering'

# Column names for table
COL_CUSTOMERID = 'CustomerId'
COL_INGREDIENTS = 'Ingredients'
COL_TIMESTAMP = 'Timestamp'
COL_LOCATION = 'Location'
COL_STATUS = 'OrderStatus'

# Session field names
SES_LOCATION = 'Location'
SES_INGREDIENTS = 'Ingredients'
SES_STATUS = 'Order_type'

# SES_STATUS constants
CONFIRM_OLD_ORDER = 'confirm_old_order'
CONFIRM_NEW_ORDER = 'confirm_new_order'
ORDER_NEW_SANDWICH = 'order_new_sandwich'

# DynamoDB Access information
AWS_KEY = 'AKIAJPHC64IUKWPGMIRQ'
AWS_PASS = 'sEnRMvpDaLD/IfCHIHO0HxWzfd5huLr0rTkfX2ar'
DYNAMODB_REGION='us-east-1'
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

# Insert order into table
def insertOrder(customerid, timestamp, ingredients, location):
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
def getOrder(customerid):
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
def delOrder(customerid):
    table = dynamodb.Table('Orders')

    response = table.delete_item(
        Key={
            COL_CUSTOMERID: customerid
        }
    )
    print("DeleteItem succeeded:")
    print(json.dumps(response, indent=4, cls=DecimalEncoder))
    return response

# --------------- Helpers that build all of the responses ----------------------

def build_speechlet_response(title, output, reprompt_text, should_end_session):
    return {
        'outputSpeech': {
            'type': 'PlainText',
            'text': output
        },
        'card': {
            'type': 'Simple',
            'title': "SessionSpeechlet - " + title,
            'content': "SessionSpeechlet - " + output
        },
        'reprompt': {
            'outputSpeech': {
                'type': 'PlainText',
                'text': reprompt_text
            }
        },
        'shouldEndSession': should_end_session
    }


def build_response(session_attributes, speechlet_response):
    return {
        'version': '1.0',
        'sessionAttributes': session_attributes,
        'response': speechlet_response
    }


# --------------- Functions that control the skill's behavior ------------------

# Welcome response
def get_welcome_response():
    # Initialize session attributes here
    session_attributes = {
        SES_LOCATION: None,
        SES_INGREDIENTS: None
    }

    card_title = "Welcome"
    speech_output = "May I take your order?"

    # If the user either does not reply to the welcome message or says something
    # that is not understood, they will be prompted again with this text
    reprompt_text = "Please tell me the order you want to place, " \
                    "by saying, I want a sandwich with, followed by a list of ingredients."
    should_end_session = False
    return build_response(session_attributes, build_speechlet_response(
        card_title, speech_output, reprompt_text, should_end_session))

# Order response
def get_order_response(intent, session, timestamp):
    session_attributes = {
        SES_LOCATION: None,
        SES_INGREDIENTS: None,
        SES_STATUS: CONFIRM_NEW_ORDER
    }

    ingredients = None
    location = None

    if 'OrderText' in intent['slots']:
        ingredients = intent['slots']['OrderText']['value']
        session_attributes[SES_INGREDIENTS] = ingredients

    if 'Location' in intent['slots']:
        location = intent['slots']['Location']['value']
        session_attributes[SES_LOCATION] = location

    if (ingredients is not None and location is not None):
        speech_output = "You ordered a sandwich with " + \
                    ingredients + ' which will be delivered to ' + location + \
                        ". Is that correct?"
        reprompt_text = "Is your order correct?"

    should_end_session = False

    return build_response(session_attributes, build_speechlet_response(
        intent['name'], speech_output, reprompt_text, should_end_session))

# Check order response
def get_check_order_response(intent, session, timestamp):
    card_title = intent['name']
    session_attributes = {
        SES_LOCATION: '',
        SES_INGREDIENTS: '',
        SES_STATUS: ''
    }
    should_end_session = False

    customerid = session['user']['userId']
    response = getOrder(customerid)

    if ('Item' in response):
        ingredients = response['Item'][COL_INGREDIENTS]
        location = response['Item'][COL_LOCATION]
        status = response['Item'][COL_STATUS]
        session_attributes[SES_INGREDIENTS] = ingredients
        session_attributes[SES_LOCATION] = location
        session_attributes[SES_STATUS] = CONFIRM_OLD_ORDER
        if (status == STATUS_PENDING):
            speech_output = "You have a sandwich with " + ingredients + \
                        " that is pending. " + \
                        "Would you like to cancel your order?"
            reprompt_text = "Would you like to cancel your order?"
        elif (status == STATUS_DELIVERING):
            speech_output = "You have a sandwich with " + ingredients + \
                        " which is currently being delivered."
            reprompt_text = None
            should_end_session = True
    else:
        session_attributes[SES_STATUS] = ORDER_NEW_SANDWICH
        speech_output = "There were no pending orders associated with you. " + \
                    "Would you like to order a sandwich?"
        reprompt_text= "There were no pending orders associated with you. " + \
                    "Would you like to order a sandwich?"

    return build_response(session_attributes, build_speechlet_response(
        card_title, speech_output, reprompt_text, should_end_session))

# Handle yes intent
def handle_yes_response(intent, session, timestamp):
    reprompt_text = None
    ingredients = None
    location = None
    order_type = None
    speech_output = ""
    should_end_session = True

    session_attributes = session['attributes']

    # Get attributes from session
    ingredients = session['attributes'][SES_INGREDIENTS]
    location = session['attributes'][SES_LOCATION]
    session_status = session['attributes'][SES_STATUS]

    # Cancel old order
    if (session_status == CONFIRM_OLD_ORDER):
        customerid = session['user']['userId']
        delOrder(customerid)
        speech_output = "Your order was removed from the queue. Thank you and goodbye!"

    # Confirm new order
    elif (session_status == CONFIRM_NEW_ORDER):
        customerid = session['user']['userId']
        insertOrder(customerid, timestamp, ingredients, location)
        speech_output = "Your order was placed. Please wait for your sandwich to be delivered."

    elif (session_status == ORDER_NEW_SANDWICH):
        return get_welcome_response()

    return build_response(session_attributes, build_speechlet_response(
        intent['name'], speech_output, reprompt_text, should_end_session))

def handle_no_response(intent, session, timestamp):
    reprompt_text = None
    ingredients = None
    location = None
    order_type = None
    speech_output = ""
    should_end_session = True

    session_attributes = session['attributes']

    # Get attributes from session
    ingredients = session['attributes'][SES_INGREDIENTS]
    location = session['attributes'][SES_LOCATION]
    session_status = session['attributes'][SES_STATUS]

    # Give response to old order
    if (session_status == CONFIRM_OLD_ORDER):
        speech_output = "Okay! Please wait for your sandwich."
        return build_response(session_attributes, build_speechlet_response(
            intent['name'], speech_output, reprompt_text, should_end_session))

    # Ask to take order again
    elif (session_status == CONFIRM_NEW_ORDER):
        return get_welcome_response()


    return handle_session_end_request()

def handle_session_end_request():
    card_title = "Session Ended"
    speech_output = "Thank you and goodbye!"

    # Setting this to true ends the session and exits the skill.
    should_end_session = True
    return build_response({}, build_speechlet_response(
        card_title, speech_output, None, should_end_session))

# --------------- Events ------------------

# Called when session starts
def on_session_started(session_started_request, session):

    print("on_session_started requestId=" + session_started_request['requestId']
          + ", sessionId=" + session['sessionId'])

# Called when launched without specifying any info
def on_launch(launch_request, session):

    print("on_launch requestId=" + launch_request['requestId'] +
          ", sessionId=" + session['sessionId'])

    # Dispatch to skill's launch
    return get_welcome_response()

# Called when intent specified
def on_intent(intent_request, session):

    print("on_intent requestId=" + intent_request['requestId'] +
          ", sessionId=" + session['sessionId'])

    timestamp = intent_request['timestamp']
    intent = intent_request['intent']
    intent_name = intent_request['intent']['name']

    # Dispatch to skill's intent handlers
    if intent_name == "OrderIntent":
        return get_order_response(intent, session, timestamp)
    elif intent_name == "CheckOrderIntent":
        return get_check_order_response(intent, session, timestamp)
    elif intent_name == "AMAZON.YesIntent":
        return handle_yes_response(intent, session, timestamp)
    elif intent_name == "AMAZON.NoIntent":
        return handle_no_response(intent, session, timestamp)
    elif intent_name == "AMAZON.StartOverIntent":
        return get_welcome_response()
    elif intent_name == "AMAZON.HelpIntent":
        return get_welcome_response()
    elif intent_name == "AMAZON.CancelIntent" or intent_name == "AMAZON.StopIntent":
        return handle_session_end_request()
    else:
        raise ValueError("Invalid intent")

# Called when user ends session
def on_session_ended(session_ended_request, session):

    print("on_session_ended requestId=" + session_ended_request['requestId'] +
          ", sessionId=" + session['sessionId'])
    # add cleanup logic here


# --------------- Main handler ------------------

def lambda_handler(event, context):
    """ Route the incoming request based on type (LaunchRequest, IntentRequest,
    etc.) The JSON body of the request is provided in the event parameter.
    """
    print("event.session.application.applicationId=" +
          event['session']['application']['applicationId'])

    """
    Uncomment this if statement and populate with your skill's application ID to
    prevent someone else from configuring a skill that sends requests to this
    function.
    """
    # if (event['session']['application']['applicationId'] !=
    #         "amzn1.echo-sdk-ams.app.[unique-value-here]"):
    #     raise ValueError("Invalid Application ID")

    if event['session']['new']:
        on_session_started({'requestId': event['request']['requestId']},
                           event['session'])

    if event['request']['type'] == "LaunchRequest":
        return on_launch(event['request'], event['session'])
    elif event['request']['type'] == "IntentRequest":
        return on_intent(event['request'], event['session'])
    elif event['request']['type'] == "SessionEndedRequest":
        return on_session_ended(event['request'], event['session'])
