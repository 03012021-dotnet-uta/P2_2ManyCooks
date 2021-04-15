import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { AuthService } from './auth.service';

// class MockAuthService extends AuthService {
//   constructor() {
//     let r: Router;
//     let s: UserService;
//     let u: UrlService;
//     super(r, s, u);
//   }
// }

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule]
    });
    service = TestBed.inject(AuthService);
  });


//   beforeEach(async(() => {
//     TestBed.configureTestingModule({
//       imports: [Router],
//       providers: [{
//         provide: AuthService,
//         useClass: MockAuthService
//       }]
//     })
//       .compileComponents();
//   }));


//   beforeEach(() => {
//     TestBed.configureTestingModule({});
//     service = TestBed.inject(AuthService);
//   });

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });
// });
