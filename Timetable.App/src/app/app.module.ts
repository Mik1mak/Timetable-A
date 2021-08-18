import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor, TimetableHourPipe } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register/register.component';
import { ShereComponent } from './shere/shere.component';
import { GroupsComponent } from './groups/groups.component';
import { SettingsComponent } from './settings/settings.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ToastsComponent } from './toasts/toasts.component';
import { GroupComponent } from './group/group.component';
import { GroupModalAddComponent } from './group-modal-add/group-modal-add.component';
import { GroupModalEditComponent } from './group-modal-edit/group-modal-edit.component';
import { ClipboardModule } from 'ngx-clipboard';
import { WeeksComponent } from './weeks/weeks.component';
import { WeekComponent } from './week/week.component';
import { DayComponent } from './day/day.component';
import { LessonComponent } from './lesson/lesson.component';
import { WeeksModalAddLessonComponent } from './weeks-modal-add-lesson/weeks-modal-add-lesson.component';

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
        WeeksModalAddLessonComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }