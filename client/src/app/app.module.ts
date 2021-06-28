import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from './_modules/shared.module';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { AccountsComponent } from './budget/accounts/accounts.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { TextInputsComponent } from './_forms/text-inputs/text-inputs.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { CategoriesComponent } from './budget/categories/categories.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AccountsComponent,
    RegisterComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TextInputsComponent,
    DateInputComponent,
    CategoriesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}