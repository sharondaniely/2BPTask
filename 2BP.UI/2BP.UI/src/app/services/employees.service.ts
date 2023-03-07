import { Injectable } from '@angular/core';
import {HttpClient}  from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmployeesListItem } from '../models/employees-list-item.models';
import { Employee } from '../models/employee.models';
import { Observable } from 'rxjs';

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
}
