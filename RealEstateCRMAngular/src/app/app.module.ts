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
import { LeadDetailsComponent } from './lead/lead-details/lead-details.component';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    LeadComponent,
    LeadDetailsComponent
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
