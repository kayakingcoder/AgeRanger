import { Component, Inject, NgModule } from '@angular/core';
import { Http } from '@angular/http';
import { ButtonModule } from 'primeng/primeng';
import { InputTextModule, DataTableModule, SharedModule } from 'primeng/primeng';

import { TabViewModule, PanelModule, SelectItem } from 'primeng/primeng';

import { Person, AgeGroup } from './person';
import { PersonService } from './person.service';

class PrimePerson implements Person {
    id: number;
    firstName: string;
    lastName: string;
    age?: number;
    ageGroup: AgeGroup;

    constructor() { }
}

class PrimeAgeGroup implements AgeGroup {
    id: number;
    minAge?: number;
    maxAge?: number;
    description: string;
}

@Component({
    selector: 'person',
    templateUrl: './person.component.html',
    providers: [PersonService]
})
export class PersonComponent {
    persons: Person[]; //People? sticking to type name
    currentPerson: Person;
    bob: Person;
    searchValue: string;
    displayDialog: boolean = false;
  
    constructor(private personService: PersonService) {
        this.personService.GetAll("").then(people => {
            this.persons = people;
        });
    }

    search() {
        this.personService.GetAll(this.searchValue).then(people => {
            this.persons = people;
        });
    }

    save() {

        if (this.currentPerson.id == 0) {
            this.personService.Add(this.currentPerson).then(newPerson => {
                //this.persons.push(newPerson);
                //Not ideal, but I'm out of time
                this.personService.GetAll("").then(people => {
                    this.persons = people;
                });
            });
        } else {
            this.personService.Update(this.currentPerson).then(newPerson => {
            });
        }
        this.hideDialog();
    }

    addPersonDialog() {
        this.currentPerson = new PrimePerson();
        this.currentPerson.id = 0;
        this.displayDialog = true;
    }

    onRowSelect() {
        this.displayDialog = true;
    }

    hideDialog() {
        this.displayDialog = false;

    }

}
