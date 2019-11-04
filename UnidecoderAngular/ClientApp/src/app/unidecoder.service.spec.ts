import { TestBed } from '@angular/core/testing';

import { UnidecoderService } from './unidecoder.service';

describe('UnidecoderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnidecoderService = TestBed.get(UnidecoderService);
    expect(service).toBeTruthy();
  });
});
