import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Config, Word } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getBestWord(words: Word[]): Observable<string> {
    const serializableWords = words.map(w => {
      return {
        value: w.value,
        letterPatterns: w.letterPatterns.map(p => p.value),
      };
    });

    return this.http
      .post('api/wordsolver', serializableWords, { responseType: 'text' })
      .pipe(catchError(this.handleError));
  }

  getConfigurationValues(): Observable<Config> {
    return this.http
      .get<Config>('api/config')
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    const errorMessage =
      error.status === 0
        ? 'An error occurred:'
        : `Backend returned code ${error.status}, body was: `;

    console.error(errorMessage, error.error);

    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }
}
