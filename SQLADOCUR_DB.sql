use AdoCrud_Db
/*Create Procedure SP_Employee*/

Create table Departments(id int primary key identity,
name varchar(max) 
) 


Create table Employees
(id int primary key identity,
  Name varchar(max),
  Email varchar(max),
  Gender varchar(20),
  Mobile varchar(100)
)









alter Procedure SP_Employees
@action varchar(40),
@id int =0,
@Name varchar(100)=null,
@Email varchar(100)=null,
@Mobile varchar(100)=null,
@Gender varchar(100)=null
as
begin
if(@action='CREATE')
begin
INSERT INTO Employees(Name,Email,Mobile,Gender) values
(@name,@Email,@Mobile,@Gender)
select 1 as result
end

else if(@action='DELETE')
begin
delete from Employees where id=@id
select 1 as result 
end
else if(@action='SELECT')
begin
select * from Employees
end
else if(@action='SELECT_SINGLE')
begin
select * from Employees Where id=@id;
select 1 as result
end
else if(@action='UPDATE')
begin
update Employees set Name=@Name,Email=@Email,Mobile=@Mobile,
Gender=@Gender where id = @id
select 1 as result
end 

else if(@action='SELECT_JOIN')
begin
select E.id, E.Name,E.Email,E.Mobile,Gender from Employees E inner join 
Departments D on E.id=D.id
end
end