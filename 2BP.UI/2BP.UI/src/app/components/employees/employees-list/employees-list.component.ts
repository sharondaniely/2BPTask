import { Component, OnInit } from '@angular/core';
import { EmployeesListItem } from 'src/app/models/employees-list-item.models';
import { EmployeesService } from 'src/app/services/employees.service';


@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit{
  employees : EmployeesListItem[] = [];  
  isLoading=true;
  constructor (private employeeService: EmployeesService) {}


  ngOnInit(): void {
    this.employeeService.getEmployeesList().subscribe({
      next: (employees)=>{
        this.employees=employees;
        
      },
      error: (response)=> {
        console.log(response);
    
      },
      complete: ()=>{
        this.isLoading=false;
      }

      
      
    });


  }

}
