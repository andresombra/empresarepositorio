import React, { useEffect, useState } from 'react';

type Empresa = {
  empresaId: number;
  nome: string;
  email: string;
  dataCadastro: string;
  contato: string;
  endereco: string;
};

const API_URL = 'https://localhost:5000/api/Empresa'; // Altere para o endpoint real

export default function EmpresaCrud() {
  const [empresas, setEmpresas] = useState<Empresa[]>([]);
  const [form, setForm] = useState<Partial<Empresa>>({});
  const [editingId, setEditingId] = useState<number | null>(null);

  useEffect(() => {
    fetchEmpresas();
  }, []);

  function fetchEmpresas() {
    fetch(API_URL)
      .then(res => res.json())
      .then(setEmpresas);
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const method = editingId ? 'PUT' : 'POST';
    const url = editingId ? `${API_URL}/${editingId}` : API_URL;
    fetch(url, {
      method,
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(form),
    }).then(() => {
      setEditingId(null);
      setForm({});
      fetchEmpresas();
    });
  }

    function handleEdit(empresa: Empresa) {
        setEditingId(empresa.empresaId);
    setForm({
      nome: empresa.nome,
      email: empresa.email,
      dataCadastro: empresa.dataCadastro.slice(0, 10),
      contato: empresa.contato,
      endereco: empresa.endereco,
    });
  }

  function handleDelete(id: number) {
    fetch(`${API_URL}/${id}`, { method: 'DELETE' }).then(fetchEmpresas);
  }

  return (
    <div>
      <h2>Empresas</h2>
      <form onSubmit={handleSubmit}>
        <input
          name="nome"
          placeholder="Nome"
          value={form.nome || ''}
          onChange={handleChange}
          maxLength={250}
          required
        />
        <input
          name="email"
          placeholder="E-mail"
          value={form.email || ''}
          onChange={handleChange}
          maxLength={300}
          type="email"
        />
        <input
          name="dataCadastro"
          placeholder="Data de Cadastro"
          value={form.dataCadastro || ''}
          onChange={handleChange}
          type="date"
          required
        />
        <input
          name="contato"
          placeholder="Contato"
          value={form.contato || ''}
          onChange={handleChange}
          maxLength={15}
        />
        <input
          name="endereco"
          placeholder="Endereço"
          value={form.endereco || ''}
          onChange={handleChange}
          maxLength={300}
        />
        <button type="submit">{editingId ? 'Atualizar' : 'Cadastrar'}</button>
        {editingId && (
          <button onClick={() => { setEditingId(null); setForm({}); }} type="button">
            Cancelar
          </button>
        )}
      </form>
      <table className="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>E-mail</th>
            <th>Data Cadastro</th>
            <th>Contato</th>
            <th>Endereço</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {empresas.map(empresa => (
            <tr key={empresa.empresaId}>
              <td>{empresa.empresaId}</td>
              <td>{empresa.nome}</td>
              <td>{empresa.email}</td>
              <td>{empresa.dataCadastro ? new Date(empresa.dataCadastro).toLocaleDateString('pt-BR') : ''}</td>
              <td>{empresa.contato}</td>
              <td>{empresa.endereco}</td>
              <td>
                <button onClick={() => handleEdit(empresa)}>Editar</button>
                <button onClick={() => handleDelete(empresa.empresaId)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}