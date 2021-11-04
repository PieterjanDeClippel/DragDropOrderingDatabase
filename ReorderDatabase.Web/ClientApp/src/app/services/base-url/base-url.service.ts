import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseUrlService {

  constructor(@Inject(DOCUMENT) document: any) {
    this.document = document;
  }

  private document: Document;
  public get baseUrl() {
    let baseTags = this.document.getElementsByTagName('base');
    if (baseTags.length === 0) {
      return null;
    } else {
      console.log('href', baseTags[0].href.slice(0, -1));
      return baseTags[0].href.slice(0, -1);
    }
  }
}
