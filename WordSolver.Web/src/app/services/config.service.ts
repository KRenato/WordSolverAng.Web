import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { ApiService } from '.';
import { Config } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  private values!: Config;

  constructor(private apiService: ApiService) {}

  public get(): Observable<Config> {
    if (!this.values) {
      return this.apiService
        .getConfigurationValues()
        .pipe(tap(v => (this.values = v)));
    }

    return of(this.values);
  }
}
