import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../http.service';
import { Lead } from '../models/lead';

@Component({
  selector: 'app-lead',
  templateUrl: './lead.component.html',
  styleUrls: ['./lead.component.css']
})
export class LeadComponent implements OnInit {
  
  leads: Lead[];
  foundLeads: Lead[] = [];

  searchText: string
  

  selectedLead: Lead;

 
  sellerLeads: Lead[] = [];
  buyerLeads: Lead[] = [];

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

  onSelect(lead: Lead): void {
    this.selectedLead = lead;
    console.log(lead);
  
  }

  sort(){
    this.leads.forEach(element => {
      if(element.LeadType === "Buyer"){
        console.log("buyer: "+element);
        this.buyerLeads.push(element);
      }
      else if(element.LeadType === "Seller"){
        console.log("seller: "+element);
        this.sellerLeads.push(element);
      }
      else{
        console.log(element);
      }
    });
  }

  isEmptyObject(obj) {
    console.log(obj);
    return (obj && (Object.keys(obj).length === 0));
  }

}
