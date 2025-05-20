import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface Empresa {
  empresaId: number;
  nome: string;
  email: string;
  dataCadastro: string;
  contato: string;
  endereco: string;
}

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {
  private apiUrl = `${environment.apiUrl}/empresa`;

  constructor(private http: HttpClient) { }

  getEmpresas(): Observable<Empresa[]> {
    return this.http.get<Empresa[]>(this.apiUrl);
  }

  getEmpresa(id: number): Observable<Empresa> {
    return this.http.get<Empresa>(`${this.apiUrl}/${id}`);
  }

  addEmpresa(empresa: Empresa): Observable<Empresa> {
    return this.http.post<Empresa>(this.apiUrl, empresa);
  }

  updateEmpresa(id: number, empresa: Empresa): Observable<Empresa> {
    return this.http.put<Empresa>(`${this.apiUrl}/${id}`, empresa);
  }

  deleteEmpresa(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
