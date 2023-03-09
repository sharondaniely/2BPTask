import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { EmployeePageComponent } from './components/employees/employee-page/employee-page.component';
import { ModalComponent } from './components/report-modal/modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TaskModalComponent } from './components/task-modal/task-modal/task-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesListComponent,
    EmployeePageComponent,
    ModalComponent,
    TaskModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule
  ],
  entryComponents:[
    ModalComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
