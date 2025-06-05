import { Component, OnInit } from '@angular/core';
import { EmpresaService, Empresa } from '../services/empresa.service';

@Component({
  selector: 'app-empresa-list',
  templateUrl: './empresa-list.component.html'
})
export class EmpresaListComponent implements OnInit {
  empresas: Empresa[] = [];

  constructor(private empresaService: EmpresaService) { }

  ngOnInit(): void {
    this.empresaService.getEmpresas().subscribe(data => this.empresas = data);
  }

  deleteEmpresa(id: number) {
    this.empresaService.deleteEmpresa(id).subscribe(() => {
      this.empresas = this.empresas.filter(e => e.empresaId !== id);
    });
  }
}
