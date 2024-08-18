import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { students } from '../types/student';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
   apiUrl="http://localhost:5070/api/students";

  constructor(private http:HttpClient) { }
  getStudents=():Observable<students[]>=> this.http.get<students[]>(this.apiUrl)

  addStudent=(data:students)=> this.http.post(this.apiUrl,data);
  
  getStudent=(id:number):Observable<students>=> this.http.get<students>(this.apiUrl+'/'+id);
  
  deleteStudent=(id:number)=> this.http.delete(this.apiUrl+'/'+id);
  editStudent=(id:number,data:students)=> this.http.put(this.apiUrl+'/'+id,data);

}
