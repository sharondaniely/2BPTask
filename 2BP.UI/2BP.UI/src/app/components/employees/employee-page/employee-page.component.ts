import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/models/employee.models';
import { EmployeesService } from 'src/app/services/employees.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from '../../report-modal/modal.component';
import { TaskModalComponent } from '../../task-modal/task-modal/task-modal.component';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent {
  
  isLoading=true;
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

  constructor(
    private route: ActivatedRoute, 
    private employeeService: EmployeesService, 
    private modalService: NgbModal
    ){}

  openReportModal() {
    let modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.currEmployee=this.currEmployee;

  }


  openTaskModal(employeeId:string) {
    let modalRef = this.modalService.open(TaskModalComponent);
    modalRef.componentInstance.currEmployee=this.currEmployee;
    modalRef.componentInstance.targetEmplyeeId=employeeId;

  }

  ngOnInit():void{
    this.route.paramMap.subscribe({
      next: (params)=>{
        const id= params.get('id');
        if(id){
          this.employeeService.getEmployeeData(id).subscribe({
            next: (response) =>{
              this.currEmployee=response;
            },
            error: (response)=> {
              console.log(response);
            }
          });
          
        }
        
      },
      error: (err)=>{
        console.log(err);
      }

    })
  }


}
