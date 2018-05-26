import { Component, OnInit } from '@angular/core';
import {HttpService} from '../http.service';
import { User } from '../models/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {


  constructor(private _httpService: HttpService) { }

  ngOnInit() {
  }

  

}
