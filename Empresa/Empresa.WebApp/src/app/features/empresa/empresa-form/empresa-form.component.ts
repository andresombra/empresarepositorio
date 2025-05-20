import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmpresaService, Empresa } from '../services/empresa.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-empresa-form',
  templateUrl: './empresa-form.component.html'
})
export class EmpresaFormComponent implements OnInit {
  empresaForm: FormGroup;
  empresaId?: number;

  constructor(
    private fb: FormBuilder,
    private empresaService: EmpresaService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.empresaForm = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(250)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(300)]],
      dataCadastro: ['', Validators.required],
      contato: ['', [Validators.required, Validators.maxLength(15)]],
      endereco: ['', [Validators.required, Validators.maxLength(300)]]
    });
  }

  ngOnInit(): void {
    this.empresaId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.empresaId) {
      this.empresaService.getEmpresa(this.empresaId).subscribe(empresa => {
        this.empresaForm.patchValue(empresa);
      });
    }
  }

  onSubmit() {
    if (this.empresaForm.invalid) return;

    const empresa: Empresa = this.empresaForm.value;
    if (this.empresaId) {
      this.empresaService.updateEmpresa(this.empresaId, empresa).subscribe(() => {
        this.router.navigate(['/empresas']);
      });
    } else {
      this.empresaService.addEmpresa(empresa).subscribe(() => {
        this.router.navigate(['/empresas']);
      });
    }
  }
}
