import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  form!: FormGroup;

    get f():any{
      return this.form.controls;
    }
  constructor(public formBuilder: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmaSenha')
    };

    this.form = this.formBuilder.group({
      titulo: ['', Validators.required],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      email: ['',
        [
          Validators.required,
          Validators.email
        ]
      ],
      telefone: ['', Validators.required],
      funcao: ['', Validators.required],
      descricao: ['', Validators.required],
      userName: ['', Validators.required],
      senha: ['',
        [
          Validators.required,
          Validators.minLength(6)
        ]
      ],
      confirmaSenha: ['', Validators.required]
    }, formOptions);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }
  }
  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

}
