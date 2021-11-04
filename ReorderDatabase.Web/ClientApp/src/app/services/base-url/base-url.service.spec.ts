import { TestBed } from '@angular/core/testing';

import { BaseUrlService } from './base-url.service';

describe('BaseUrlService', () => {
  let service: BaseUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
