import { Component, OnInit } from '@angular/core';
import { EmployeesListItem } from 'src/app/models/employees-list-item.models';
import { EmployeesService } from 'src/app/services/employees.service';


@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit{
  // mockEmployees : EmployeesListItem[] =[];
  employees : EmployeesListItem[] = [];  
  mockEmployees : EmployeesListItem[] =
    [
      {
        id: "1",
        firstName: "Stiv",
        lastName: "Jobs",
        position: "CEO"
      },
      {
        id: "2",
        firstName: "Matt",
        lastName: "Dum",
        position: "CTO"
      },
      {
        id: "3",
        firstName: "Lori",
        lastName: "Thin",
        position: "CFO"
      },
      {
        id: "4",
        firstName: "Alice",
        lastName: "the First",
        position: "CPO"
      },
      {
        id: "5",
        firstName: "Bob",
        lastName: "Dillen",
        position: "CMO"
      },
    ]

  constructor (private employeeService: EmployeesService) {}

  ngOnInit(): void {
    this.employeeService.getEmployeesList().subscribe({
      next: (employees)=>{
        this.employees=employees;
      },
      error: (response)=> {
        console.log(response);
      }
      
    });


  }

}
