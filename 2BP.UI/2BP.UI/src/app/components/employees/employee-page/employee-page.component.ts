import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/models/employee.models';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent {
    currEmployee : Employee = {
    id: '',
    firstName: '',
    lastName:'',
    position: '',
    pictureURL: '',
    managerId: '',
    managerName: '',
    taskList: [],
    subordinates: [],
    reports:[]
  };

  constructor(private route: ActivatedRoute, private employeeService: EmployeesService){}

  ngOnInit():void{
    this.route.paramMap.subscribe({
      next: (params)=>{
        const id= params.get('id');
        console.log(params);
        if(id){
          this.employeeService.getEmployeeData(id).subscribe({
            next: (response) =>{
              this.currEmployee=response;
            }
          })
        }
      }
    })
  }


}
