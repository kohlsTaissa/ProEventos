import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private toastr: ToastrService
  ){}

  canActivate(): boolean {
    if(localStorage.getItem('user') !== null) return true;

    this.toastr.info('Espertinho, tem que fazer login pra acessar o site', 'Atenção');
    this.router.navigate(['/user/login']);
    return false;
  }

}
