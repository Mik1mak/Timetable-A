import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor, TimetableHourPipe } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { ShereComponent } from './shere';
import { GroupsComponent } from './groups';
import { SettingsComponent } from './settings';
import { NavbarComponent } from './navbar';
import { ToastsComponent } from './toasts';
import { GroupComponent } from './group';
import { GroupModalAddComponent } from './group-modal-add';
import { GroupModalEditComponent } from './group-modal-edit';
import { ClipboardModule } from 'ngx-clipboard';
import { WeeksComponent } from './weeks';
import { WeekComponent } from './week';
import { DayComponent } from './day';
import { LessonComponent } from './lesson';
import { WeeksModalAddLessonComponent } from './weeks-modal-add-lesson';
import { ConfirmModalComponent } from './confirm-modal';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        FormsModule,
        ClipboardModule,
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        ShereComponent,
        GroupsComponent,
        SettingsComponent,
        NavbarComponent,
        ToastsComponent,
        GroupComponent,
        GroupModalAddComponent,
        GroupModalEditComponent,
        WeeksComponent,
        WeekComponent,
        DayComponent,
        LessonComponent,
        TimetableHourPipe,
        WeeksModalAddLessonComponent,
        ConfirmModalComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }