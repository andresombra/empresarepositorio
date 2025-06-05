import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmpresaListComponent } from './features/empresa/empresa-list/empresa-list.component';
import { EmpresaFormComponent } from './features/empresa/empresa-form/empresa-form.component';
import { EmpresaDetailComponent } from './features/empresa/empresa-detail/empresa-detail.component';

const routes: Routes = [
  { path: 'empresas', component: EmpresaListComponent },
  { path: 'empresas/new', component: EmpresaFormComponent },
  { path: 'empresas/edit/:id', component: EmpresaFormComponent },
  { path: 'empresas/:id', component: EmpresaDetailComponent },
  { path: '', redirectTo: '/empresas', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


