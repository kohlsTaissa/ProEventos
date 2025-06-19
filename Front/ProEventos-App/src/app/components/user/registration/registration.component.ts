import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { User } from '@app/models/identity/User';
import { AccountService } from '@app/services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  user = {} as User;
  form!: FormGroup;

  get f():any{
    return this.form.controls;
  }

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmaPassword')
    };

    this.form = this.formBuilder.group({
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['',
        [
          Validators.required,
          Validators.email
        ]
      ],
      userName: ['', Validators.required],
      password: ['',
        [
          Validators.required,
          Validators.minLength(4)
        ]
      ],
      confirmaPassword: ['', Validators.required]
    }, formOptions);
  }

  register(): void{
    this.user = {...this.form.value};
    this.accountService.register(this.user).subscribe(
      () => this.router.navigateByUrl('/dashboard'),
      (error: any) => this.toastr.error(error.error)
    )
  }

}
