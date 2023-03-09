import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse}  from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmployeesListItem } from '../models/employees-list-item.models';
import { Employee } from '../models/employee.models';
import { Report } from '../models/report.models';
import { Observable } from 'rxjs';
import { WorkTask } from '../models/work-task.models';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  baseApiUrl: string= environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getEmployeesList() : Observable<EmployeesListItem[]> {
    return this.http.get<EmployeesListItem[]>(this.baseApiUrl +'/api/employee');
  }

  getEmployeeData(id: string) : Observable<Employee> {
    return this.http.get<Employee>(this.baseApiUrl +'/api/employee/'+id);
  }

  sendReport(report:Report): Observable<HttpResponse<string>>
  {
    return this.http.post<HttpResponse<string>>(this.baseApiUrl +'/api/employee/report',report);
  }

  assignTask(task:WorkTask): Observable<HttpResponse<string>>
  {
    return this.http.post<HttpResponse<string>>(this.baseApiUrl +'/api/employee/assign',task);
  }
}
