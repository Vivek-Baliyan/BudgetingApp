import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UsersService } from '../_services/users.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private usersService: UsersService,
    private toastr: ToastrService
  ) {}
  canActivate(): Observable<boolean> {
    return this.usersService.currentUser$.pipe(
      map((user) => {
        if (user) return true;

        this.toastr.error('You shall not pass!');
      })
    );
  }
}
