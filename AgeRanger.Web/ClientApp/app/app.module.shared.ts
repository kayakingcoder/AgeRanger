import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { PersonComponent } from './components/person/person.component';
import { PersonService } from './components/person/person.service';


import { ButtonModule, InputTextModule, DataTableModule, SharedModule } from 'primeng/primeng';
import { DialogModule, PanelModule } from 'primeng/primeng';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PersonComponent,
        HomeComponent,

    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ButtonModule,
        InputTextModule,
        DataTableModule,
        SharedModule,
        DialogModule,
        PanelModule,
        BrowserAnimationsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'person', component: PersonComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [PersonService]
})
export class AppModuleShared {
}
