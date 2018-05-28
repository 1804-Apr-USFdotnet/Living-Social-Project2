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

  selectedLead: Lead;

  testLeads: Lead[] = [
    {id: null, User: null, Address: null, City:'Dallas', State: 'Texas', Zipcode: 12345, LeadName: 'Cooper', EmailAddress: 'test@me.com', LeadType: 'buyer', PriorApproval: false, PhoneNumber: '1111111111'}
  ];

  constructor(private _httpService: HttpService) { }


  ngOnInit() {
    // this.getLeads();
    
  }

  // getLeads(): void{
  //   this._httpService.getLeads()
  //     .subscribe(
  //         response => this.leads = response,
  //         errors => console.log(errors)
  //     );
  // }

  // searchLeads(){
  //   this._httpService.searchLeads(this.searchText)
  //     .subscribe(
  //       response => this.leads = response,
  //       error => console.log(error)
  //     );
  // }

  onSelect(lead: Lead): void {
    this.selectedLead = lead;
    console.log(lead);
  }

}
