import { Component, OnInit, Input, inject } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Report } from 'src/app/models/report.models';
import { Employee } from 'src/app/models/employee.models';
import { EmployeePageComponent } from '../employees/employee-page/employee-page.component';
import { Guid } from 'guid-typescript';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
  
})

export class ModalComponent implements OnInit {

  @Input() public currEmployee : Employee = {
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

  closeResult = '';
  
  report : Report={
    id: '',
    data: '',
    creationDate : new Date(),
    assignTo : '',
    creator : ''

  }
  constructor(
    public activeModal: NgbActiveModal, private employeeService: EmployeesService
  ) {}

  ngOnInit(): void {
    this.report.creator=  this.currEmployee.id;
    this.report.assignTo= this.currEmployee.managerId;
  }

  sendReport():void {
    if(this.report.data==''){
      alert('message cannot be empty');
      return;
    }
    this.report.creationDate= new Date();
    this.report.id=Guid.create().toString();
    this.employeeService.sendReport(this.report).subscribe( {
      next: (response)=>{
            this.activeModal.close();
      },
      error: (err: any) => {
        console.log(err);
      }
      
    });
  }
}