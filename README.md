# File Cabinet Application
## Description
--------------------------------------------------------
A console application that accepts user commands and manages user data, as well as stores records with personal information about a person.

## Available command line parameters
--------------------------------------------------------
**Storage type** (-s, --storage):
- file
- memory (default value)
```
-s file
--storage=memory
```
**Validation rules** (-v, --validation-rules):
- custom
- default (default value)
```
-v CuStOm
--validation-rules=Default
```
**Method call log** ( --use-logger)  
**Performance monitoring** ( --use-stopwatch)

# Commands
--------------------------------------------------------
**Help**

Show all available commands.

**Create**

Saves data to a record and returns the ID of the new record.

*Usage example:*
```
> create
...
Record #1 is created.
```
**Stat**

Displays the total number of records and the number of deleted records.

*Usage example:*
```
> stat
91 record(s), 8 of them deleted.
```
**Export**

Exports service data to CSV or XML file formats.

*Usage example:*
```
> export csv c:\users\myuser\records.csv
All records are exported to file c:\users\myuser\records.csv.

> export xml e:\filename.xml
File is exist - rewrite e:\filename.xml? [Y/n] Y
All records are exported to file filename.xml.
```
**Import**

Imports service data to CSV and XML file formats.

*Usage example:*
```
> import csv d:\data\records.csv
10000 records were imported from d:\data\records.csv.

> import xml d:\data\records2.xml
Import error: file d:\data\records.xml not exist.
```
**Purge**

Performs data file defragmentation - removing "voids" in the data file. (Works only with db-file)

*Usage example:*
```
> purge
Data file processing completed: 12 of 98 records were purged.
```
**Insert**

Adds a record using the transmitted data.   
Available fields: *id, firstname, lastname, dateofbirth, gender, height, weight.*

*Usage example:*
```
> insert (id, firstname, lastname, dateofbirth, gender, height, weight) values (2, 'Will', 'Smith', '25/09/1968', 'm', 188', '90')

> insert (weight, height, gender, dateofbirth, lastname, firstname, id) values ('45', '165', 'f', '04/06/1975', 'Jolie', 'Angelina', '10')
```
**Delete**

Deletes records using the specified criteria.  
Available fields: *id, firstname, lastname, dateofbirth, gender, height, weight.*

*Usage example:*
```
> delete where id = '1'
Record #1 is deleted.

> delete where LastName='Smith'
Records #2, #3, #4 are deleted. 
```
**Update**

Updates record fields (except id) using the specified search criteria.   
Available fields: *id, firstname, lastname, dateofbirth, gender, height, weight.*

*Usage example:*
```
> update set DateOfBirth = '18/01/1986' where FirstName='Stan' and LastName='Smith'
Record(s) updated.
```
**Select**

Accepts a list of fields to display and search criteria.   
Available fields: *id, firstname, lastname, dateofbirth, gender, height, weight.*

*Usage example:*
```
> select id, firstname, lastname where firstname = 'John' and lastname = 'Doe'
+----+-----------+----------+
| Id | FirstName | LastName |
+----+-----------+----------+
|  1 | John      | Doe      |
+----+-----------+----------+

> select lastname where firstname = 'John' or lastname = 'Smith'
+----------+
| LastName |
+----------+
| Doe      |
| Smith    |
+----------+

> select 
+----+-----------+----------+-------------+--------+--------+--------+
| Id | FirstName | LastName | DateOfBirth | Gender | Height | Weight |
+----+-----------+----------+-------------+--------+--------+--------+
|  2 | Will      | Smith    | 1968-Sep-25 | M      |    188 |   90   |
|  7 | Alina     | Black    | 2005-Dec-31 | F      |    150 |   60   |
+----+-----------+----------+-------------+--------+--------+--------+
```
**Exit**

When this command is executed, the application will terminate and close. 

*Usage example:*
```
> exit
Exiting an application...
```

# Data Generator
## Description
--------------------------------------------------------
Generates data randomly, the values must correspond to the "default" set of rules. The generation of the ID field starts with the value that is set by the start-id parameter.
## Available command line parameters
--------------------------------------------------------
**Output format type (csv, xml)** (-t, --output-type)
```
-t xml
--output-type=csv
```
**Output file name** (-o, --output)
```
-o c:\users\myuser\records.xml
--output=d:\data\records.csv
```
**Amount of generated records** (-a, --records-amount)
```
-a 5000
--records-amount=10000
```
**ID value to start** (-i, --start-id)
```
-i 45
--start-id=30
```
