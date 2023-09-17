#!/bin/bash
database=ArabamComCaseDB
wait_time=60s
password=P@55w0rd

# wait for SQL Server to come up
echo importing data will start in $wait_time...
sleep $wait_time
echo importing data...

# run the init script to create the DB and the tables in /table
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i ./init.sql

for entry in "tables/*.sql"
do
  echo executing $entry
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i $entry
done

#import the data from the csv files
for entry in "datas/*.csv"
do
  # i.e: transform /data/MyTable.csv to MyTable
  shortname=$(echo $entry | cut -f 1 -d '.' | cut -f 2 -d '/')
  tableName=$database.dbo.$shortname
  echo importing $tableName from $entry
  /opt/mssql-tools/bin/bcp $tableName in $entry -c -t',' -F 2 -S 0.0.0.0 -U sa -P $password
done