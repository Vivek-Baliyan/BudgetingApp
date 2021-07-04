import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {}

  register() {
    this.usersService.register(this.model).subscribe((response) => {
      this.cancel();
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
