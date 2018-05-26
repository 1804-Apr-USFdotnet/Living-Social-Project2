import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { Lead } from '../models/lead';

@Component({
  selector: 'app-lead',
  templateUrl: './lead.component.html',
  styleUrls: ['./lead.component.css']
})
export class LeadComponent implements OnInit {
  
  private leads: Lead[];
  foundLeads: Lead[] = [];

  searchText: string;

  constructor(private _httpService: HttpService) { }


  ngOnInit() {
    this.getLeads();
    
  }

  getLeads(): void{
    this._httpService.getLeads()
      .subscribe(
          response => this.leads = response,
          errors => console.log(errors)
      );
  }

  searchLeads(){
    this._httpService.searchLeads(this.searchText)
      .subscribe(
        response => this.leads = response,
        error => console.log(error)
      );
  }

}
