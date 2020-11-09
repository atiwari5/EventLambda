import json
import constant
import sys
import logging
import pymysql
import datetime

# https://docs.aws.amazon.com/lambda/latest/dg/services-rds-tutorial.html

rds_host  = "eventdatabase.cu9s0xzhp0zw.eu-west-2.rds.amazonaws.com"
name = constant.db_username
password = constant.db_password
db_name = constant.db_name

logger = logging.getLogger()
logger.setLevel(logging.INFO)

try:
    conn = pymysql.connect(rds_host, user=name, passwd=password, db=db_name, connect_timeout=5)
except pymysql.MySQLError as e:
    logger.error("ERROR: Unexpected error: Could not connect to MySQL instance.")
    logger.error(e)
    sys.exit()       

logger.info("SUCCESS: Connection to RDS MySQL instance succeeded")


def readMessage(event, context):

    for record in event['Records']:
       
       #records are python dict - https://docs.aws.amazon.com/lambda/latest/dg/with-sqs.html 
       #print('test')
       payload=record["body"]
       
       #print(str(payload)) 
       #print(type(payload))

       ## We have received payload , convert into JSON object 

       dicpayload = json.loads(str(payload))
       #print(type(dicpayload))

       print(dicpayload["Event_Type"])
       #store values
       
       
       
       dicpayload["Policy"]
       dicpayload["Event_Eff_Date"]
       dicpayload["Api_Key"]
       dicpayload["Source_Req_Key"] 

       item_count = 0

       with conn.cursor() as cur:
           cur.execute('insert into EventTrigger (Event_Type , Event_Time ,Policy ,Event_Eff_Date,Api_Key,Source_Req_Key,Ref_Id,CreationDate,Status,Received_time) values(%s, %s, %s,%s, %s, %s,%s, %s, %s,%s)',(dicpayload["Event_Type"], dicpayload["Event_Time"] , dicpayload["Policy"] , dicpayload["Event_Eff_Date"] , dicpayload["Api_Key"] , dicpayload["Source_Req_Key"] , dicpayload["Id"], dicpayload["CreationDate"] , "U" , datetime.datetime.now() ))
           conn.commit()
           cur.execute("select * from EventTrigger")
           for row in cur:
               item_count += 1
               logger.info(row)
       conn.commit()




if __name__ == "__main__":
    readMessage('', '')
    