
/*
import { Component } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent {
  employees:Employee[]=[];

  const_employees:Employee[]=[

      {
        id: '1234-234-2345',
        name: 'Sharon',
        email: 'Sharon@gmail.com',
        phone: 78675758,
        salary:20000,
        department:'Dev'
      },
      {
        id: '1234-6789-3467',
        name: 'Jone',
        email: 'Jones@gmail.com',
        phone: 66573872,
        salary:25000,
        department:'Dev'
      },
      {
        id: '1234-789-2345',
        name: 'Mark',
        email: 'MarkZu@gmail.com',
        phone: 1234567,
        salary:50000,
        department:'Business'
      },
      {
        id: '1234-1111-2345',
        name: 'kathy',
        email: 'kathy@gmail.com',
        phone: 12345678987654,
        salary:4200,
        department:'Marketing'
      },
      {
        id: '1234-777-2345',
        name: 'moshe',
        email: 'moshe@gmail.com',
        phone: 4676465,
        salary:23000,
        department:'Dev'
      }


    ]

    constructor(private employeesService:EmployeesService) {}
    
    ngOnInit(): void {
      this.employeesService.getAllEmployees().subscribe({
        next: (employees)=> {
          console.log(employees);
        },
        error: (response) =>{
          console.log(response);
        }

      })

    }
}
*/

import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../models/employee.model';
import { EmployeeService } from '../../../services/employees.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  pageTitle = 'Employee List';
  filteredEmployees: Employee[] = [];
  employees: Employee[] = [];
  errorMessage = '';

  _listFilter = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredEmployees = this.listFilter ? this.performFilter(this.listFilter) : this.employees;
  }

  constructor(private employeeService: EmployeeService) { }

  performFilter(filterBy: string): Employee[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.employees.filter((employee: Employee) =>
      employee.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  ngOnInit(): void {
    this.getEmployeeData();
  }

  getEmployeeData() {
    this.employeeService.getEmployees()
      .subscribe({
        next: (employees) => {
          this.employees = employees;
          this.filteredEmployees = employees;
        },
        error: (err) => this.errorMessage = <any>err,
        complete: () => console.info('Get employees in employee list')
      });
  }

  deleteEmployee(id: string, name: string): void {
    if (id === '') {
      this.onSaveComplete();
    } else {
      if (confirm(`Are you sure want to delete this Employee: ${name}?`)) {
        this.employeeService.deleteEmployee(id)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: (err) => this.errorMessage = <any>err,
            complete: () => console.info('Delete employee in employee list')
          });
      }
    }
  }

  onSaveComplete(): void {
    this.employeeService.getEmployees()
      .subscribe({
        next: (employees) => {
          this.employees = employees;
          this.filteredEmployees = employees;
        },
        error: (err) => this.errorMessage = <any>err,
        complete: () => console.info('Get employees in employee list')
      });
  }
}