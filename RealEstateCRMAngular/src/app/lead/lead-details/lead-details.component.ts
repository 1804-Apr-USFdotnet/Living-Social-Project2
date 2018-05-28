import { Component, OnInit } from '@angular/core';
import {HttpService} from '../../http.service';
import { Lead } from '../../models/lead';

@Component({
  selector: 'app-lead-details',
  templateUrl: './lead-details.component.html',
  styleUrls: ['./lead-details.component.css']
})
export class LeadDetailsComponent implements OnInit {
  // @Input() lead: Lead;
  

  constructor(private _httpService: HttpService) { }

  lead: Lead;
  lead_id: number;
  ngOnInit() {
    this.getLead(this.lead_id);
  }

  getLead(lead_id: number){
    this._httpService.getDetails(lead_id).subscribe(
      response => {
        this.lead = response
      },
      error => {
        console.log(error)
      }
    )
  }

}
