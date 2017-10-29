import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Person } from './person';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class PersonService {
    baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    GetAll(filter: string) {
        if (filter == undefined) {
            filter = "";
        }
        return this.http.get(this.baseUrl + '/api/v1/person?q=' + filter)
            .toPromise()
            .then(res => <Person[]>res.json())
            .then(data => { return data; });
    }

    Add(person: Person) : Promise<Person> {

        return this.http.post(this.baseUrl + '/api/v1/person', person)
            .map(response => response.json())
            .toPromise()
            .then(res => <Person>res)
            .then(data => { return data; });
    }

    Update(person: Person): Promise<Person> {

        return this.http.put(this.baseUrl + '/api/v1/person/' + person.id, person)
            .map(response => response.json())
            .toPromise()
            .then(res => <Person>res)
            .then(data => { return data; });
    }

    private extractData(res: any) {
        console.log("extractage");
        let body = res.json();
        return body.data || {};
    }

    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Promise.reject(errMsg);
    }


}