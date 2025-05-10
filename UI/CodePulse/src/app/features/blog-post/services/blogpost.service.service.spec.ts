import { TestBed } from '@angular/core/testing';

import { BlogpostService } from './blogpost.service';

describe('BlogpostServiceService', () => {
  let service: BlogpostService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogpostService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
