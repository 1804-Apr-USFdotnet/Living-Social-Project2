import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';
import { LeadComponent } from './lead/lead.component';
import { LeadDetailsComponent } from './lead/lead-details/lead-details.component';

const routes : Routes = [
  {path: 'leads/:LeadId', component: LeadDetailsComponent},
  {path: 'leads', component: LeadComponent},
  {path: '', redirectTo: '/', pathMatch: 'full'}
]
@NgModule({
  imports: [

    RouterModule.forRoot(routes)
  ],
  declarations: [],
  exports:[
    RouterModule
  ]
})
export class AppRoutingModule { }
