import { ApplicationConfig, ErrorHandler, LOCALE_ID, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeIn from '@angular/common/locales/en-IN';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { GlobalErrorHandler } from './core/interceptors/global-error.handler';
import { httpErrorInterceptor } from './core/interceptors/http-error.interceptor';

registerLocaleData(localeIn);

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: LOCALE_ID, useValue: 'en-IN' },
    provideZonelessChangeDetection(),
    provideRouter(routes),
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor, httpErrorInterceptor])
    )
  ]
};
