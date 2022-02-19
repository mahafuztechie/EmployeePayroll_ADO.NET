CREATE PROCEDURE spUpdateEmp
(
	@BasicPay decimal,
	@Name VARCHAR(200),
	@PhoneNumber varchar(50),
	@address varchar(50),
	@department varchar(50),
	@Deduction decimal(10,2),
	@TaxablePay decimal(10,2),
	@Tax decimal(10,2),
	@NetPay decimal(10,2)
	
)
AS
Begin 
UPDATE emp_payroll
SET Salary = @BasicPay, phonenumber = @PhoneNumber, address=@address, department=@department, deductions=@Deduction, taxable_pay=@TaxablePay, income_tax=@Tax, net_pay=@NetPay
WHERE Name = @Name
End 

go

CREATE PROCEDURE spGetDataByDate
(
	@StartDate date,
	@EndDate date
)
AS
Begin 
SELECT * FROM emp_payroll
WHERE StartDate BETWEEN CAST('08-06-2010' AS DATE ) AND GETDATE();
End
go

CREATE or ALTER PROCEDURE spGetDataByDateRange
(
	@StartDate date,
	@EndDate date
)
AS
Begin 
SELECT * FROM emp_payroll
WHERE StartDate BETWEEN CAST(@StartDate AS DATE ) AND CAST(@EndDate AS DATE);
End 

go

CREATE or ALTER PROCEDURE spGetNumericCalculationsByGender
(
	@Gender varchar(1)
	
)
AS
Begin 
SELECT SUM(Salary) AS SALARY, AVG(taxable_pay) AS AVERAGE, 
        MIN(income_tax) AS MINIMUM, MAX(net_pay) AS MAXIMUM, COUNT(ID) AS COUNTS
    FROM emp_payroll WHERE Gender = @Gender  GROUP BY Gender;

End 



CREATE or ALTER PROCEDURE spAddEmpPayrollDetails
(
	@employee_name varchar(30),
	@phone_no varchar(15),
	@address varchar(100),
	@gender varchar(10),
	@basic_pay decimal(10,2),	
	@deduction decimal(10,2),
    @taxable_pay decimal(10,2),
	@income_tax decimal(10,2),
    @net_pay decimal(10,2),
	@payroll_Id varchar(20),
	@department_Id int,
	@departmentName varchar(20),
	@emp_id int
	
)
AS
BEGIN

	
	INSERT INTO Employee values(@emp_id, @employee_name, @gender, @phone_no, @address);
	INSERT INTO Payroll values(@payroll_Id, @basic_pay, @deduction, @taxable_pay, @income_tax, @net_pay,  (SELECT employee_id FROM Employee WHERE employee_id=(SELECT MAX(employee_id) FROM Employee)));
	INSERT INTO Department values(@department_Id, @departmentName,  (SELECT employee_id FROM Employee WHERE employee_id=(SELECT MAX(employee_id) FROM Employee)));
	

END