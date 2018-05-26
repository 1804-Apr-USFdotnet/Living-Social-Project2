import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'


import { HttpService } from './http.service'
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { LeadComponent } from './lead/lead.component';
import { AppRoutingModule } from './/app-routing.module';
import { HttpClient } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    LeadComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
