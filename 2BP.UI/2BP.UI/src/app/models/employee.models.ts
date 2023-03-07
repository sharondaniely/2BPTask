import { EmployeesListComponent } from "../components/employees/employees-list/employees-list.component";
import { EmployeesListItem } from "./employees-list-item.models";
import { Report } from "./report.models";
import { WorkTask } from "./work-task.models";

export interface Employee {
id: string,
firstName: string,
lastName:string,
position: string,
pictureURL: string,
managerId: string,
managerName: string,
taskList: WorkTask[],
subordinates: EmployeesListItem[],
reports: Report[]
}