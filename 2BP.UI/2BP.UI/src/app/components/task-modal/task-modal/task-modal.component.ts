import { Component, OnInit, Input, inject } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { WorkTask } from 'src/app/models/work-task.models';
import { Employee } from 'src/app/models/employee.models';
import { Guid } from 'guid-typescript';
import { EmployeesService } from 'src/app/services/employees.service';
import { NgbAlertModule, NgbDatepickerModule, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-modal',
  templateUrl: './task-modal.component.html',
  styleUrls: ['./task-modal.component.css']
})


export class TaskModalComponent implements OnInit {
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

  today: Date= new Date();
  

  dueDateSelected: NgbDateStruct={
    year: this.today.getFullYear(),
    month: this.today.getMonth()+1,
    day:this.today.getDate()
  }

  targetEmplyeeId='';

  closeResult = '';
  
  task : WorkTask={
    id: '',
    data: '',
    creationDate : new Date(),
    dueDate: new Date(),
    assignTo : '',
    creator : ''

  }
  constructor(
    public activeModal: NgbActiveModal, private employeeService: EmployeesService
  ) {}

  ngOnInit(): void {
    this.task.creator=  this.currEmployee.id;
    this.task.assignTo= this.targetEmplyeeId; 
  }

  assignTaskTrigger():void {
    if(this.task.data==''){
      alert('Task cannot be empty');
      return;
    }
    this.task.creationDate= new Date();
    this.task.id=Guid.create().toString();
    this.task.dueDate= new Date(this.dueDateSelected.year, this.dueDateSelected.month-1,this.dueDateSelected.day);
    this.employeeService.assignTask(this.task).subscribe( {
      next: (response)=>{
            this.activeModal.close();
      },
      error: (err: any) => {
        console.log(err);
      }
      
    });
  }
}
