import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';
import { LeadComponent } from './lead/lead.component';

const routes : Routes = [
  
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
