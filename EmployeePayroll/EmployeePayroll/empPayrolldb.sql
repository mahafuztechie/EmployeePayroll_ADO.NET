---uc1--create database-----employee payroll--------
create DATABASE Employee_payroll;

use Employee_payroll;
----uc2----create table----- employee payroll------
CREATE TABLE emp_payroll(
Id int IDENTITY(1,1) PRIMARY KEY,
Name varchar(200),
Salary Decimal,
StartDate Date
);

-----uc3-----perform crud--------
insert into emp_payroll (Name, Salary, StartDate)
values('john', 35000, '03-09-2010'),('joe', 45000, '08-06-2015');

------uc4----view table----
select * from emp_payroll;
select * from emp_payroll where Name='joe' or Name='james';

----uc5----retrieve data between date range--------
select * from emp_payroll where StartDate between cast('2014-01-02' as date) and getdate();

insert into emp_payroll (Name, Salary, StartDate)
values('alexa', 55000, '2020-02-04'),('roshni', 40000, '2021-12-12');

delete from emp_payroll where Id=5 or Id=6;

----uc6----add gender field------
Alter table emp_payroll add Gender varchar(1);
update emp_payroll set Gender='M' where Id=2 or Id=1;
update emp_payroll set Gender='F' where Id=3 or Id=4;

-----uc7-----find sum avg max min count of employee salary group by gender---------
select sum(Salary) as TotalSalary from emp_payroll;
select sum(Salary) as Salary, Gender from emp_payroll group by Gender;
select sum(Salary) as FemaleSalary from emp_payroll where Gender='F';
select avg(Salary) as AverageSalary, Gender from emp_payroll group by Gender;
select max(Salary) as MaxSalary, Gender from emp_payroll group by Gender;
select min(Salary) as MinSalary, Gender from emp_payroll group by Gender;
select count(Name) as NumOfEmp, Gender from emp_payroll group by Gender;

--usecase8
--Extend employee payroll table
ALTER TABLE emp_payroll
	ADD phonenumber varchar(50) NOT NULL DEFAULT(' ');
ALTER TABLE emp_payroll
	ADD address VARCHAR(50) NOT NULL DEFAULT(' ');
ALTER TABLE emp_payroll
	ADD department VARCHAR(50) NOT NULL DEFAULT(' ');
SELECT * FROM emp_payroll;

--usecase9
--Extend empployee payroll table

ALTER TABLE emp_payroll
	ADD deductions DECIMAL(10,2) NOT NULL DEFAULT(0);
ALTER TABLE emp_payroll
	ADD  taxable_pay DECIMAL(10,2) NOT NULL DEFAULT(0);
ALTER TABLE emp_payroll
	ADD  income_tax DECIMAL(10,2) NOT NULL DEFAULT(0);
ALTER TABLE emp_payroll
	ADD  net_pay DECIMAL(10,2) NOT NULL DEFAULT(0);
SELECT * FROM emp_payroll;


---uc10---insert data---------
INSERT INTO emp_payroll 
	VALUES('sachin',35000,'2019-10-31','M','123456789','Mumbai','Marketing',1000,14000,1400,12600);
	SELECT * FROM emp_payroll WHERE Name='sachin';

	--usecase11
-- drop & recreate the tables according to ER Diagram
DROP TABLE emp_payroll;

--Creating table Company
CREATE TABLE Company(
    company_id INT NOT NULL PRIMARY KEY,
    company_name VARCHAR(30) NOT NULL,
);
SELECT * FROM Company;

--Inserting data into table Company
INSERT INTO Company(company_id,company_name)
    VALUES(101,'BridgeLabz'),(102,'Capgemini'),(103,'Google');

--Creating table Employee
CREATE TABLE Employee(
    employee_id INT  NOT NULL PRIMARY KEY,
    employee_name VARCHAR(30) NOT NULL,
    gender VARCHAR(10) NOT NULL,
    phone_no VARCHAR(15),
    address VARCHAR(100)
);
INSERT INTO Employee(employee_id,employee_name,gender,phone_no,address)
    VALUES(1,'sachin','M','9899125354','Mumbai'),(2,'shalini','F','7896534218','Delhi'),
            (3,'bill','M','6578923546','Moscow'),(4,'roshni','F','8796345567','bangalore');
SELECT * FROM Employee;
 
--Creating table Department
CREATE TABLE Department(
    department_id INT NOT NULL,
    department_name VARCHAR(50) NOT NULL,
    employee_id INT FOREIGN KEY REFERENCES Employee(employee_id)
);

--Inserting into table Department
INSERT INTO Department(department_id,department_name,employee_id)
    VALUES(333,'.Net Rpa',1),(444,'Java',2),(222,'Sales',3),(555,'Marketing',4);

	select * from Department;

--Creating table Payroll
CREATE TABLE Payroll(
    payroll_id VARCHAR(20) NOT NULL PRIMARY KEY,
    basic_pay DECIMAL(10,2),
    deduction DECIMAL(10,2),
    taxable_pay DECIMAL(10,2),
    tax DECIMAL(10,2),
    net_pay DECIMAL(10,2),
    employee_id INT FOREIGN KEY REFERENCES Employee(employee_id)
);
--Inserting into table Payroll
INSERT INTO Payroll(payroll_id,basic_pay,deduction,taxable_pay,tax,net_pay,employee_id)
    VALUES('#1245',10000,500,9500,950,8550,1),('#8765',15000,750,14250,1425,12825,2),
            ('#7689',25000,3000,22000,2200,19800,3),('#9008',29000,3500,25500,2550,22950,4);

--LETS CHECK THE TABLE AND ITS FIELDS
SELECT * FROM Company;
SELECT * FROM Department;
SELECT * FROM Employee;
SELECT * FROM Payroll;

--Redoing usecase7 to perform database functions and use group by function
--Using joins
SELECT SUM(basic_pay) AS SALARY_F, AVG(taxable_pay) AS AVERAGE_F, 
        MIN(tax) AS MINIMUM_F, MAX(net_pay) AS MAXIMUM_F, COUNT(payroll_id) AS COUNT_PAYROLL
    FROM Payroll p INNER jOIN Employee e
    ON p.employee_id=e.employee_id
    WHERE e.gender = 'F' GROUP BY e.gender;

SELECT SUM(basic_pay) AS SALARY_M, AVG(taxable_pay) AS AVERAGE_M, 
        MIN(tax) AS MINIMUM_M, MAX(net_pay) AS MAXIMUM_M, COUNT(payroll_id) AS COUNT_PAYROLL
    FROM Payroll p INNER jOIN Employee e
    ON p.employee_id=e.employee_id
    WHERE e.gender = 'M' GROUP BY e.gender;

--usecase12
SELECT * FROM Payroll;