import { Component, OnInit } from '@angular/core';
import {HttpService} from '../../http.service';
import { Lead } from '../../models/lead';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lead-details',
  templateUrl: './lead-details.component.html',
  styleUrls: ['./lead-details.component.css']
})
export class LeadDetailsComponent implements OnInit {
  // @Input() lead: Lead;
  
  lead: Lead;

  constructor(private _httpService: HttpService, private _activatedRoute: ActivatedRoute) { }


  ngOnInit() {
    let LeadId = this._activatedRoute.snapshot.params['LeadId'];
    this.getLead(LeadId);
    console.log(this.lead);
  }

  getLead(LeadId: number){
    this._httpService.getDetails(LeadId).subscribe(
      response => {
        this.lead = response
      },
      error => {
        console.log(error)
      }
    )
  }

}
