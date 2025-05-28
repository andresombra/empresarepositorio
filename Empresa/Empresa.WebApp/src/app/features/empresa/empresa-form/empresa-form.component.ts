import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmpresaService, Empresa } from '../services/empresa.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-empresa-form',
  templateUrl: './empresa-form.component.html'
})
export class EmpresaFormComponent implements OnInit {
  empresaForm!: FormGroup;
  isEdit = false;
  empresaId?: number;

  constructor(
    private fb: FormBuilder,
    private empresaService: EmpresaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.empresaForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dataCadastro: ['', Validators.required],
      contato: ['', Validators.required],
      endereco: ['', Validators.required]
    });
  
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEdit = true;
        this.empresaId = +id;
        this.empresaService.getEmpresa(this.empresaId).subscribe(empresa => {
          this.empresaForm.patchValue(empresa);
        });
      }
    });
  }

  onSubmit() {
    if (this.empresaForm.invalid) return;

    const empresa: Empresa = {
      empresaId: this.empresaId ?? 0,
      ...this.empresaForm.value
    };

    if (this.isEdit && this.empresaId) {
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
