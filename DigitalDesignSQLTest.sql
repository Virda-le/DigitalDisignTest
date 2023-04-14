use [EmployeeDB]
--Напишите запросы, которые выведут:

--Сотрудника с максимальной заработной платой.

Select TOP 1 
ID, ENAME
FROM EMPLOYEE
ORDER BY SALARY DESC
go

--Вывести одно число: максимальную 
--длину цепочки руководителей по таблице сотрудников (вычислить глубину дерева).
go
create view Boss
as
select distinct e1.ID,e1.ENAME,e1.CHIEF_ID
from Employee as e1 join Employee as e2
on e1.ID = e2.CHIEF_ID
go
with tree(id_T, chId ,lvl)
as
(
select Id,CHIEF_ID,1
from Boss
union all
select Boss.Id, Boss.CHIEF_ID,tree.lvl +1
from Boss inner join tree
on tree.id_T = [Boss].CHIEF_ID
)
select Max(lvl) as max_level_Of_tree
from tree

--Сотрудника, чье имя начинается на «Р» и заканчивается на «н».
select *
from Employee
where ENAME like N'Р%н'

--Отдел, с максимальной суммарной зарплатой сотрудников. 			
Select dp.ID,dp.DName,sm.sumOf
from Department as dp join (Select top 1 DEPARTMENT_ID, Sum(Salary) as sumOf
			from Employee 			
			group by DEPARTMENT_ID
			order by sumOf desc) as sm
			on sm.DEPARTMENT_ID = dp.ID