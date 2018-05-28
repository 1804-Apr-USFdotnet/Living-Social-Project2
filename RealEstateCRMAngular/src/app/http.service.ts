import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Lead } from './models/lead';





@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private _httpclient: HttpClient) { 
    
  }
  private urlBase = "http://localhost:57955/api/Leads/ng";

  getLeads(onFail = (reason) => console.log(reason)) : Observable<Lead[]>{
    var r = this._httpclient.get<Lead[]>(this.urlBase);
    return r;
  }
  getDetails(lead_id: number): Observable<Lead>{
    return this._httpclient.get<Lead>("http://localhost:57955/api/Leads/"+lead_id)
  }

  searchLeads(email:string): Observable<Lead[]>{
    var r = this._httpclient.get<Lead[]>(this.urlBase + "/" + email);
    return r;
  }

  

}
