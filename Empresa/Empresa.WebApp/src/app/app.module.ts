import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmpresaListComponent } from './features/empresa/empresa-list/empresa-list.component';
import { EmpresaFormComponent } from './features/empresa/empresa-form/empresa-form.component';
import { EmpresaDetailComponent } from './features/empresa/empresa-detail/empresa-detail.component';
//import { EmpresaComponent } from './empresa/empresa-list/empresa/empresa.component';

@NgModule({
  declarations: [
    AppComponent,
    EmpresaListComponent,
    EmpresaFormComponent,
    EmpresaDetailComponent
  //  EmpresaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
